using System.Security.Cryptography;

using RSA rsa = RSA.Create(2048);

byte[] privateKeyBytes = rsa.ExportPkcs8PrivateKey();
byte[] publicKeyBytes = rsa.ExportSubjectPublicKeyInfo();

Console.WriteLine("PRIVATE KEY (base64):");
Console.WriteLine(Convert.ToBase64String(privateKeyBytes));
Console.WriteLine();
Console.WriteLine("PUBLIC KEY (base64):");
Console.WriteLine(Convert.ToBase64String(publicKeyBytes));