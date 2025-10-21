# Conceptos Fundamentales de Autenticación y Autorización en ASP.NET Core

Este documento explica las diferencias clave entre los mecanismos de seguridad de ASP.NET Core, enfocándose en la autenticación basada en Claims y el uso de Cookies.

---

## 🆚 1. Diferencias con ASP.NET Core Identity

| Concepto | Función Principal | Alcance |
| :--- | :--- | :--- |
| **ASP.NET Core Identity** | Proporciona una solución **completa** para gestionar usuarios, contraseñas, roles, tokens, confirmación de correo electrónico, etc. | API de Alto Nivel |
| **ASP.NET Core Autenticación** | Solo determina la **identidad** (*quién eres*) y crea un objeto (**ClaimsPrincipal**) que representa al usuario autenticado. | Mecanismo de Bajo Nivel |

---

## 🏷️ 2. Claims (Declaraciones)

Los **Claims** (o Declaraciones) son la **esencia de la identidad** en ASP.NET Core y en el estándar de seguridad **Claims-Based Identity** (Identidad Basada en Declaraciones).

Un **Claim** es una pieza de información que describe al usuario y se presenta como un par **Clave-Valor**:

| Tipo de Claim (Clave) | Valor (Dato) | Ejemplo de Uso |
| :--- | :--- | :--- |
| `name` | `usuario@ejemplo.com` | Identificación básica. |
| `role` | `Administrador` | Autorización basada en roles. |
| **Personalizado** (`LicenseLevel`) | `VIP` | Autorización basada en atributos específicos del negocio. |

### ¿Por qué Claims Personalizados?

Los Claims personalizados se definen para **ajustarse a las necesidades específicas de tu negocio** o aplicación, permitiendo una autorización más granular que no se limita a los roles estándar.

---

## 🍪 3. Autenticación Basada en Cookies

La **Autenticación Basada en Cookies** es un mecanismo muy común y la opción ideal para sitios web tradicionales (MVC, Razor Pages) en ASP.NET Core cuando se configura con `.AddCookie()`.

### ¿Cómo funciona el Mecanismo de las Cookies (**Stateful**)?

Este es el proceso que habilita el middleware de `AddCookie` para mantener la sesión del usuario:

1.  **Inicio de Sesión:** El usuario proporciona credenciales y el código las valida.
2.  **Creación de la Identidad:** Se construye el objeto **ClaimsPrincipal** del usuario, incluyendo todos los Claims (estándar y personalizados).
3.  **Cifrado y Almacenamiento:** El middleware de cookies **serializa, cifra** y protege la identidad completa.
4.  **Respuesta:** El servidor envía esta información cifrada al navegador dentro de una **cookie de sesión**.
5.  **Peticiones Subsecuentes:** En cada solicitud posterior, el navegador **automáticamente** adjunta esa *cookie* cifrada al encabezado HTTP.
6.  **Desencriptación:** ASP.NET Core intercepta la *cookie*, la descifra y **rehidrata** el objeto **ClaimsPrincipal**. El usuario es considerado autenticado y se procede a la autorización.