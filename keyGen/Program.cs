using System.Security.Cryptography;



static void createKeyPair()
{
    using RSA rsa = RSA.Create(2048);

    byte[] privateKeyBytes = rsa.ExportPkcs8PrivateKey();
    byte[] publicKeyBytes = rsa.ExportSubjectPublicKeyInfo();

    Console.WriteLine("PRIVATE KEY (base64):");
    Console.WriteLine(Convert.ToBase64String(privateKeyBytes));
    Console.WriteLine();
    Console.WriteLine("PUBLIC KEY (base64):");
    Console.WriteLine(Convert.ToBase64String(publicKeyBytes));

}

/*
 
 
 oQ7gwTpLnVZUFVR5TPuz4VcZ6HTI77miAkXCeVlHzgo=
   ************************************
   ck8/Tf8OJ33VBm1lwMO5rdXvJTVA/2by7qyBd4CbAns=
   ************************************
   gu5aULKnFA3i0Fby8qbOnnOMoAuYI9zpSzG3n9Tncec=
   PRIVATE KEY (base64):
   MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCnpYc8cbqIXPlfBk9u/Z+bENxk1sXf9BY2+2p277jy4OgoNj0sy8aMeQMy/RYA0qpU3+yxT0bCsqvFHKRBLO8DEHvrFAolM1m9dqiGxzTGm0Kwe7eGwVNx9+m+rFUuTLB/FXZrBMbtDk+TOEPsQkm2yFluWnYgBuD33yi46TWTqCTpCfAvBrFT7rmQki9xpVWfgZ5TFaDd40DB/4SmxIVCBERXiQxqs5k158cxKYJa4RtOvLjU3eq4Mc5jW5lNw3bR05xdc7Ovmq76YqgoZFp7fD14ouFPyC52Y6C6dr+cCsmGCgxUwHyCp7Ijslvjt1+nm7XNFsJtzK+Yt7j18/7jAgMBAAECggEAB2WGu3R7GeQcPoSNQaTgA1vxlWNidJiU2FUsY9X8z357z7Zg6ZaOComWSgwfiebBjCAbFdGdGh7+9CHo31VHsFdsWDlYnz6OQY04L0YfvnmALkAvHzttXpRlFDwPQA0zJlBAmsUpr7DXcvPjD0v3a6CYTYWhO5WCHH2Ukt/7NKn+udt2KqlEDfZr2rT1C4Bp5pFQmxjHCIiIIcKO6lwGT7duJrOr56917bnipYoTp1CsR5N8UlKnFjEuDxCqHiFjseuAm+DqcYaqka3v31TCx8zE/fXmrCalVorExZYvbR+yLFS1kCZq6libRdg3JFB8XUv5Nzj2EafAgo2HVEYpOQKBgQDT7tgkMGik7gohxAK8JWbOaYl0yrjJFvl445hsPf+nWOgJJH/eSl8rypIRVqsdGVEdkEKfga1UmJlZ/ucp4UnBKS7GWzvStI2Sqz/aT/ZECdbMAcP/V6GPsJM7R8w/fWY921F6jxNgDZae15o/cTMELACc1PCPJM/WZH4ZTWQhawKBgQDKgVO/gA/ixCQWyqxsZU2tJl46aA6+QFVs1B2jxW0zor9ejrrcdw/kFZrSz6wyDEePlYGALP0zHEB4+Xv9JIoxULX3CvsoybowVO86RwsNDcYhlcdoty6W4F1+bX2RT/2KpXaqCxbzCy0swWeSo6/moqoPFI2sICT8D1jBGRdeaQKBgQCxnN2vlpJchtIe8jKIk5/RJUl5g9vRS1vQII3BSURUb4InB1vSY+nWvXIk7cmCHZGJgTkUjI1C9JCwh5wb0R8KrwTwX82HDKIJZVOiFpmA8+38Ew67lClmTslVSRq3qtRgyslCOV43havRe0deG/Rxbp8k0KC6llNIjffQJLfh+QKBgH/3dbq/7En6dGvKq2bcJVfTtRvngdmLwe/BovFI4xcMMy5Ht6F1w574YBvzi/ccd2Qur+UViPNxWPSZg1aWbpV3UWXJlKlTwMqmAX6sQjl/iMLUaWysxU4mfi5UdlMLX0bsBK6zz07Ks0Ni/FZYBeISzLA25sfskDKQaB+uAWxxAoGAUiK9jI7MIabnqll/+og9tn4jBouUssMEhT8xPa5uT5IS0TM9Dwcwm8UwXhIZzCGrQUMWa1eBHQUhkxUQaKpm2syOGxl0CjZ27cZc9CSVSJlS++gzsSRHoo/DaKVUWcIJwujLBMiK0CtvYct5Vk5WQoS0KNAcLmPPC7X5QhrKiSw=
   
   PUBLIC KEY (base64):
   MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAp6WHPHG6iFz5XwZPbv2fmxDcZNbF3/QWNvtqdu+48uDoKDY9LMvGjHkDMv0WANKqVN/ssU9GwrKrxRykQSzvAxB76xQKJTNZvXaohsc0xptCsHu3hsFTcffpvqxVLkywfxV2awTG7Q5PkzhD7EJJtshZblp2IAbg998ouOk1k6gk6QnwLwaxU+65kJIvcaVVn4GeUxWg3eNAwf+EpsSFQgREV4kMarOZNefHMSmCWuEbTry41N3quDHOY1uZTcN20dOcXXOzr5qu+mKoKGRae3w9eKLhT8gudmOguna/nArJhgoMVMB8gqeyI7Jb47dfp5u1zRbCbcyvmLe49fP+4wIDAQAB
   
 
 
  
 */

static void createClientSecrets()
{

    byte[] secretBytes1 = RandomNumberGenerator.GetBytes(32);
    Console.WriteLine(Convert.ToBase64String(secretBytes1));

    Console.WriteLine("************************************");
    byte[] secretBytes2 = RandomNumberGenerator.GetBytes(32);

    Console.WriteLine(Convert.ToBase64String(secretBytes2));
    Console.WriteLine("************************************");

    byte[] secretBytes3 = RandomNumberGenerator.GetBytes(32);
    Console.WriteLine(Convert.ToBase64String(secretBytes3));
}


createClientSecrets();
createKeyPair();