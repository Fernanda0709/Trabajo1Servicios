using System;
using System.Net;
using System.Net.Mail;
using UnityEngine;

public class EmailSender : MonoBehaviour
{
    public static event Action<string> OnEmailSuccess;
    public static event Action<string> OnEmailError;

    public void SendEmail(string toEmail, string subject, string body)
    {
        string fromEmail = "ingmultimediausbbog@gmail.com";
        string password = "fsjq ioqf zsxs jrzf";

        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(fromEmail);
        mail.To.Add(toEmail);
        mail.Subject = subject;
        mail.Body = body;

        SmtpClient smtp = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(fromEmail, password),
            EnableSsl = true
        };

        try
        {
            smtp.Send(mail);
            Debug.Log("Email sended succesfuly");
            OnEmailSuccess?.Invoke("Email enviado a " + toEmail);
        }
        catch (Exception ex)
        {
            Debug.Log("Error" + ex.Message);
            OnEmailError?.Invoke("El email no pudo ser enviado");
        }
    }
}