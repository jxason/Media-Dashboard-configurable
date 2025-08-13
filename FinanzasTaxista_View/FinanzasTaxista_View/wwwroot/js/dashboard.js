document.addEventListener("DOMContentLoaded", function () {
    fetch('/api/Dashboard/1')
        .then(res => res.json())
        .then(data => {
            // Resumen
            document.getElementById('saldo-actual').innerText = `$${data.resumen.saldoActual.toFixed(2)}`;
            document.getElementById('total-viajes').innerText = data.resumen.totalViajes;
            document.getElementById('total-gastos').innerText = `$${data.resumen.totalGastos.toFixed(2)}`;
            document.getElementById('dias-trabajados').innerText = data.resumen.diasTrabajados;

            // Historial agrupado por fecha
            const historialAgrupado = {};
            data.historialMes.forEach(h => {
                const fechaStr = new Date(h.fecha).toLocaleDateString();
                if (!historialAgrupado[fechaStr]) historialAgrupado[fechaStr] = { ingresos: 0, gastos: 0 };
                historialAgrupado[fechaStr].ingresos += h.ingresos;
                historialAgrupado[fechaStr].gastos += h.gastos;
            });
            const fechas = Object.keys(historialAgrupado);
            const ingresos = fechas.map(f => historialAgrupado[f].ingresos);
            const gastos = fechas.map(f => historialAgrupado[f].gastos);

            new Chart(document.getElementById('grafico-historial'), {
                type: 'line',
                data: {
                    labels: fechas,
                    datasets: [
                        { label: 'Ingresos', data: ingresos, borderColor: 'green', fill: false, tension: 0.3 },
                        { label: 'Gastos', data: gastos, borderColor: 'red', fill: false, tension: 0.3 }
                    ]
                }
            });

            // Ingresos por Categoría
            new Chart(document.getElementById('grafico-ingresos-categoria'), {
                type: 'pie',
                data: {
                    labels: data.categorias.ingresosPorCategoria.map(c => c.categoria),
                    datasets: [{
                        data: data.categorias.ingresosPorCategoria.map(c => c.monto),
                        backgroundColor: ['#4caf50', '#2196f3', '#ffc107', '#ff5722']
                    }]
                }
            });

            // Gastos por Categoría
            new Chart(document.getElementById('grafico-gastos-categoria'), {
                type: 'pie',
                data: {
                    labels: data.categorias.gastosPorCategoria.map(c => c.categoria),
                    datasets: [{
                        data: data.categorias.gastosPorCategoria.map(c => c.monto),
                        backgroundColor: ['#f44336', '#9c27b0', '#03a9f4', '#8bc34a']
                    }]
                }
            });

            // Últimos viajes
            const listaViajes = document.getElementById('lista-viajes');
            data.ultimosRegistros.ultimosViajes.forEach(v => {
                const li = document.createElement('li');
                li.classList.add('list-group-item');
                li.textContent = `${new Date(v.fecha).toLocaleDateString()} - ${v.ubicacion} - $${v.monto}`;
                listaViajes.appendChild(li);
            });

            // Últimos gastos (ignorar fecha rara 1988)
            const listaGastos = document.getElementById('lista-gastos');
            data.ultimosRegistros.ultimosGastos
                .filter(g => new Date(g.fecha).getFullYear() > 2000)
                .forEach(g => {
                    const li = document.createElement('li');
                    li.classList.add('list-group-item');
                    li.textContent = `${new Date(g.fecha).toLocaleDateString()} - ${g.categoria} - $${g.monto}`;
                    listaGastos.appendChild(li);
                });
        })
        .catch(err => console.error("Error al cargar el dashboard:", err));
});
