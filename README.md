# 📊 Media Dashboard Interactivo – Proyecto Final SC-701

Este proyecto consiste en el desarrollo de un **Media Dashboard configurable**, el cual permite gestionar y visualizar diferentes tipos de contenidos como: clima, noticias, imágenes, videos, tipos de cambio, entre otros. Además, incluye funcionalidades de configuración avanzada por usuario, autenticación, y administración de componentes multimedia personalizados.

## 👥 Integrantes del Proyecto
- Jason Zuñiga Solórzano  
- Badilla Carvajal Wendy Priscilla  
- Chacón Camacho Steven Leonel  
- Esquivel Solís Sebastián  
- Picado Urbina Daniel Gilberto  

## 🔗 Enlace al Repositorio GitHub
(https://github.com/jxason/Media-Dashboard-configurable.git)  


## 🛠 Especificación Técnica del Proyecto

### 🧱 Arquitectura del Proyecto
- **Tipo de proyecto:** ASP.NET Core 8.0 MVC con Razor Pages  
- **Patrón arquitectónico:** MVC (Modelo-Vista-Controlador)
- **Estructura en capas:**  
  - `Models` – Entidades de base de datos y ViewModels  
  - `Controllers` – Lógica de negocio y ruteo de vistas  
  - `Views` – Interfaz visual basada en Razor  
  - `Services` – Lógica de consumo de APIs externas  
  - `Data` – Contexto EF y migraciones  

### 🧩 Tecnologías, Paquetes y Librerías
- **Framework principal:** ASP.NET Core 8.0 MVC
- **Base de datos:** SQL Server
- **ORM:** Entity Framework Core
- **Frontend:** Bootstrap 5 + HTML5 + CSS3
- **Autenticación:** ASP.NET Identity
- **Consumo de APIs externas:** HttpClient
- **Automatización y temporizadores:** System.Timers
- **Responsividad:** Bootstrap + Flexbox
- **Control de dependencias:** Inyección de dependencias (DI) con `services.AddScoped<>`

### 🧠 Principios SOLID aplicados
- **S (Single Responsibility):** Controladores y servicios desacoplados por funcionalidad  
- **O (Open/Closed):** Código preparado para extensiones de tipos de widgets sin modificar el núcleo  
- **L (Liskov):** Herencia y polimorfismo aplicados en los servicios para APIs  
- **I (Interface Segregation):** Interfaces divididas según cada responsabilidad (ej: `IWidgetService`, `IUserService`)  
- **D (Dependency Inversion):** Uso de interfaces inyectadas para servicios y repositorios

### 🧱 Patrones de Diseño Usados
- **Repository Pattern** para separación lógica de acceso a datos  
- **Factory Pattern** para creación dinámica de widgets según tipo  
- **Strategy Pattern** para refresco de contenido asincrónico según configuración del usuario  

## ✅ Funcionalidades Clave

### 🔐 Autenticación y Roles
- Registro e inicio de sesión de usuarios
- Roles: Administrador, Usuario Estándar
- Seguridad mediante Identity y autorización por claims

### 📈 Dashboard Interactivo
- Visualización de todos los widgets configurados
- Widgets favoritos aparecen en la parte superior
- Posibilidad de ocultar widgets no deseados por usuario

### ⚙️ Panel de Configuración
- CRUD completo de componentes/widgets multimedia
- Personalización de tiempo de refresco, URL del API, tamaño, visibilidad y posición
- Configuración de privacidad: visibilidad por usuario, por rol o global
- Vista personalizada por usuario (dashboard customizado)

### 🌐 Consumo de APIs Externas
- API de clima (ej: OpenWeatherMap)
- API de noticias (ej: NewsAPI)
- API de tipo de cambio (ej: exchangerate.host)
- API de imágenes o videos (ej: Unsplash, YouTube Data API)

### 🕒 Automatización
- Refresco automático de widgets mediante temporizador y tareas asíncronas
- Carga dinámica sin bloqueo (uso de `async/await` + AJAX)

## 🖼️ Vistas Implementadas

- `Login / Registro` (con validaciones y gestión de sesión)
- `Dashboard` con todos los widgets visibles del usuario
- `Configuración` con CRUD completo por widget
- `User Profile` con edición de información del usuario
- Submenús y categorías en dashboard y panel

## 📋 Notas Adicionales

- Validaciones implementadas para evitar IDs duplicados
- Se respetan las convenciones de naming y estilo en C#
- Se aplicó indentación limpia y estandarizada en todo el código
- El diseño es completamente responsivo para escritorio, tablet y móviles

---

📌 Proyecto desarrollado como parte del curso **SC-701: Programación web Avanzada**, Universidad Fidélitas.  
📅 Entrega y exposición: Semana 14/15  
