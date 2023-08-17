using System;
using System.Security.Cryptography;
using System.Text;

namespace Library
{
    public class PasswordEncrypter
    {
        private string key;

        public PasswordEncrypter(string key) 
        {
            this.key = "";

            try
            {
                if (string.IsNullOrEmpty(key))
                    throw new ArgumentNullException("Se debe indicar una clave de descifrado.");

                if (key.Length != 16)
                    throw new ArgumentException("La longitud de la clave debe ser de 16 dígitos.");

                this.key = key;
            }
            catch (ArgumentNullException e)
            {
                Util.WarningMessage(e.ParamName is null ? e.Message : e.ParamName);
            }
            catch (Exception e) 
            {
                Util.WarningMessage(e.Message);            
            }
        }


        public byte[] Encrypt(string password)
        {
            byte[] encryption = new byte[] { };

            try
            {
                encryption = this.EncryptPassword(password);
            }
            catch (ArgumentNullException e)
            {
                Util.WarningMessage(e.ParamName is null ? e.Message : e.ParamName);
            }
            catch (Exception e)
            {
                Util.WarningMessage(e.Message);
            }

            return encryption;
        }


        public string Decrypt(byte[] passwordEncrypted)
        {
            string password = "";

            try
            {
                password = this.DecryptPassword(passwordEncrypted);
            }
            catch (ArgumentNullException e) 
            {
                Util.WarningMessage(e.ParamName is null ? e.Message : e.ParamName);
            }
            catch (CryptographicException e)
            {
                Util.WarningMessage(e.Message);
            }

            return password;
        }

        public string Decrypt(string passwordEncrypted)
        {
            return this.Decrypt(Encoding.UTF8.GetBytes(passwordEncrypted));
        }


        private byte[] EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) 
                throw new ArgumentNullException("Debe indicar una contraseña.");

            byte[] encrypted = new byte[] { };

            using (Aes aes = Aes.Create()) 
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = Encoding.UTF8.GetBytes(key);

                // Creo el objeto de la clase AES que cifra el texto
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                // Creo el flujo de memoria y cifrado. El flujo de memoria se utiliza para almacenar el texto cifrado, y el flujo de cifrado se utiliza para cifrar el texto.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) // Escribe en el flujo de memoria
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            // Escribe el string en el flujo de cifrado y memoria
                            swEncrypt.Write(password);
                        }

                        // Obtiene la contraseña cifrada del flujo de memoria
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }


        private string DecryptPassword(byte[] passwordEncrypted)
        {
            if (passwordEncrypted is null || passwordEncrypted.Length == 0)
                throw new ArgumentNullException("Debe indicar una clave cifrada");     

            string password = "";

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = Encoding.UTF8.GetBytes(key);

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(passwordEncrypted))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string
                            try
                            {
                                password = srDecrypt.ReadToEnd();
                            }
                            catch (CryptographicException)
                            {
                                throw new CryptographicException("La clave o el cifrado no coinciden.");
                            }
                        }
                    }
                }
            }

            return password;
        }


        public string PasswordEncryptedToString(byte[] encrypted)
        {
            return Encoding.UTF8.GetString(encrypted);
        }
    }
}