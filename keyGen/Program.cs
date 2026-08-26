using System.Security.Cryptography;


/*
using RSA rsa = RSA.Create(2048);

byte[] privateKeyBytes = rsa.ExportPkcs8PrivateKey();
byte[] publicKeyBytes = rsa.ExportSubjectPublicKeyInfo();

Console.WriteLine("PRIVATE KEY (base64):");
Console.WriteLine(Convert.ToBase64String(privateKeyBytes));
Console.WriteLine();
Console.WriteLine("PUBLIC KEY (base64):");
Console.WriteLine(Convert.ToBase64String(publicKeyBytes));

*/





byte[] secretBytes1 = RandomNumberGenerator.GetBytes(32);
Console.WriteLine(Convert.ToBase64String(secretBytes1));

Console.WriteLine("************************************");
byte[] secretBytes2 = RandomNumberGenerator.GetBytes(32);
Console.WriteLine(Convert.ToBase64String(secretBytes2));
Console.WriteLine("************************************");

byte[] secretBytes3 = RandomNumberGenerator.GetBytes(32);
Console.WriteLine(Convert.ToBase64String(secretBytes3));
