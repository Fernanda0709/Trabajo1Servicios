# Pinball 3D — Sistema de Notificaciones SMTP

Este es un proyecto hecho en Unity que integra un sistema de notificaciones por correo electrónico usando SMTP. El juego envía correos automáticamente en dos momentos clave: cuando el jugador supera los 10 puntos y cuando ocurre un Game Over.

---

## 📁 Estructura de Scripts

```
Assets/Scripts/
├── SERVICIOS/
│   ├── EmailSender.cs          → Envío SMTP el código base para enviar el email
│   ├── GameManager.cs          → Lógica del juego + construcción dinámica del mensaje
│   ├── GameOver.cs             → Trigger de fin de partida
│   ├── UIControllerSMTP.cs     → Panel de email en la UI
│   └── MainThreadDispatcher.cs → Puente entre hilo SMTP y hilo principal de Unity
└── OTROS/
    ├── Score.cs                → Muestra el puntaje en pantalla
    └── ChangePositionBall.cs   → Suma puntos al jugador
```

---

## 🔔 Eventos que disparan notificaciones

El sistema tiene **dos eventos** que generan un correo:

| **Puntaje alto** | Cuando el jugador supera 10 puntos | `GameManager.NotificarPuntajeAlto()` |
| **Game Over** | Cuando la bola cae en la zona de muerte | `GameManager.NotificarGameOver()` |

En ambos casos el correo incluye el puntaje actual y la fecha, construidos dinámicamente en tiempo de ejecución.

---

## 📨 Flujo básico de envío SMTP

```
[Jugador ingresa su correo en el panel UI]
          │
          ▼
[Presiona el botón PLAY]
   - El panel se oculta
   - El correo queda guardado en GameManager.emailDestino
          │
          ▼
[Durante el juego — evento ocurre]
          │
          ▼
[EmailSender.SendEmail(toEmail, subject, body)]
   - Construye MailMessage con asunto y cuerpo dinámicos
   - Configura SmtpClient con smtp.gmail.com:587
   - Llama smtp.Send(mail)
          │
     ┌────┴────┐
   Éxito     Error
     │          │
  Debug.Log  Debug.Log
  OnEmailSuccess  OnEmailError
          │
[UIControllerSMTP] muestra el resultado en pantalla
```

---

## ⚙️ Manejo de respuestas del servidor

`EmailSender.cs` maneja dos casos:

### Éxito
```csharp
smtp.Send(mail);
Debug.Log("Email sent successfully");
OnEmailSuccess?.Invoke("Correo enviado exitosamente a " + toEmail);
// → UI muestra el mensaje en verde
```

### Error
```csharp
catch (Exception ex)
{
    Debug.Log("Error: " + ex.Message);
    OnEmailError?.Invoke("El correo no pudo ser enviado, intentalo de nuevo");
    // → UI muestra el mensaje en rojo
}
```

Errores comunes del servidor SMTP:

| Código | Descripción |
|---|---|
| 535 | Credenciales incorrectas |
| 550 | Destinatario rechazado |
| 550-5.4.5 | Límite diario de envíos alcanzado |
| 421 | Servicio no disponible |

---

## Interfaz de usuario

El panel SMTP está integrado en la escena principal del juego y permite:

- **Ingresar el correo destino** — campo de texto TMP_InputField
- **Activar el juego** — botón PLAY que oculta el panel e inicia la partida
- **Visualizar el estado del envío** — texto que cambia a verde (éxito) o rojo (error)

---

##  Flujo de uso

1. Abrir la escena `SampleScene` en Unity
2. Escribir un correo destino en el panel
3. Presionar **PLAY** — el panel se oculta
4. Jugar y acumular puntos — al superar 10 llega el primer correo
5. Perder — llega el correo de Game Over con el puntaje final
6. Revisar el correo destino para confirmar la recepción

---
---

*Taller SMTP — Ingeniería Multimedia — Unity 6*
