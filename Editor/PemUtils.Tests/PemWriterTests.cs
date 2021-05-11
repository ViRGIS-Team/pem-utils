﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;

namespace PemUtils.Tests
{
    [TestFixture]
    public class PemWriterTests
    {
        [Test]
        public void WritePublicKey_PublicRsaParameters_ShouldWriteCorrectKey()
        {
            var expectedPem = "-----BEGIN PUBLIC KEY-----\n"
                + "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAsiLoIxmXaZAFRBKtHYZh\n"
                + "iF8m+pYR+xGIpupvsdDEvKO92D6fIccgVLIW6p6sSNkoXx5J6KDSMbA/chy5M6pR\n"
                + "vJkaCXCI4zlCPMYvPhI8OxN3RYPfdQTLpgPywrlfdn2CAum7o4D8nR4NJacB3NfP\n"
                + "nS9tsJ2L3p5iHviuTB4xm03IKmPPqsaJy+nXUFC1XS9E/PseVHRuNvKa7WmlwSZn\n"
                + "gQzKAVSIwqpgCc+oP1pKEeJ0M3LHFo8ao5SuzhfXUIGrPnkUKEE3m7B0b8xXZfP1\n"
                + "N6ELoonWDK+RMgYIBaZdgBhPfHxF8KfTHvSzcUzWZojuR+ynaFL9AJK+8RiXnB4C\n"
                + "JwIDAQAB\n"
                + "-----END PUBLIC KEY-----\n";

            // Data has been derived from https://superdry.apphb.com/tools/online-rsa-key-converter
            var rsa = new RSAParameters
            {
                Modulus = Convert.FromBase64String("siLoIxmXaZAFRBKtHYZhiF8m+pYR+xGIpupvsdDEvKO92D6fIccgVLIW6p6sSNkoXx5J6KDSMbA/chy5M6pRvJkaCXCI4zlCPMYvPhI8OxN3RYPfdQTLpgPywrlfdn2CAum7o4D8nR4NJacB3NfPnS9tsJ2L3p5iHviuTB4xm03IKmPPqsaJy+nXUFC1XS9E/PseVHRuNvKa7WmlwSZngQzKAVSIwqpgCc+oP1pKEeJ0M3LHFo8ao5SuzhfXUIGrPnkUKEE3m7B0b8xXZfP1N6ELoonWDK+RMgYIBaZdgBhPfHxF8KfTHvSzcUzWZojuR+ynaFL9AJK+8RiXnB4CJw=="),
                Exponent = Convert.FromBase64String("AQAB")
            };

            using (var stream = new MemoryStream())
            {
                using (var writer = new PemWriter(stream))
                    writer.WritePublicKey(rsa);

                stream.Seek(0, SeekOrigin.Begin);

                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    var pem = reader.ReadToEnd();
                    Assert.That(pem, Is.EqualTo(expectedPem));
                }
            }
        }

        [Test]
        public void WritePrivateKey_PrivateRsaParameters_ShouldWriteCorrectKey()
        {
            var expectedPem = "-----BEGIN RSA PRIVATE KEY-----\n"
                + "MIIEpAIBAAKCAQEAzw2rOycv+DqPhxJ/XZDsALH0WIn/Yyk97TGKhYME6CuybqlJ\n"
                + "BVTbpd2th2Fw/bDTwIHXr2wYJubX9b4G0gfVIwemq1rZyyJu2SxoOEK4BQg5s8po\n"
                + "Qh0Uq5KadP5DVvxk0AkIUySBCvNU+AHxRFJtJ6UZTjtUVYv2Yie+3YWhN3uDZ5ki\n"
                + "mjB7VSZqgdgnMpfx8kDTtGFHpA1oQNt71z3nyXPRTxZXGlYZlgGNEmv+cd3wYuuQ\n"
                + "0LnqZf3thXZfXYd0ASWhfdq0BxGT8WbThk8y6y9/aHsEtaf4QSpc8idVXPu0BwR8\n"
                + "P4p5lGzY11a5YERPOCl5BiaHYPgUJxzPnkFipwIDAQABAoIBAA26MUUNtw91CnkB\n"
                + "D/KrHgp5weJw276+SD3GkBGD+zpNU1ok3RN+acWYad3U5wHazF8x/JPDzeIeYekH\n"
                + "/TnFjSryYelwb4oZMVIysIIyYjLrNbAm1jyz4t/xK05gYSSOPTzRrHyeqfOI6HQ8\n"
                + "5LsL3/LF7mSSaGf3jJE7Y1sadfLQneSUhLMjDksl+o914P/WrV/LGcZ7O6noZ3Vg\n"
                + "e8zPGlPciPVOBqHqtWzxyTxbtycIUPW7GKbZjaWMwRwLnI5U+hcoGQgHsRUUJMcw\n"
                + "iGubcScdlOHCnorxM79sgjnko4RD+DUZGU/if/oNRS6dhIvaWCIrt+4lHhpHhZGI\n"
                + "d16tLCECgYEA+uOWmE91jbACXylQ+GSAchUfAmeqtf2e23TlXzScleEay/elSbf6\n"
                + "/HgUhUiUXCa2r+3d6kbEhoKy4Hed1Jm19rYBBIMSLZR0R3Il987x6WAnx+xrkfSW\n"
                + "I3Z8fHJoCtTfS/hXA+W/Snr0WlFUMgwaEZzd3FEIU62ZizKgjO1c67ECgYEA00V5\n"
                + "L2a7FGa684RWXxNmFX0ROmAn3Dt9urT8lRsfsRARF52dPqSthS/iLchBIGTBUtZD\n"
                + "5EnX5ttu88o5XkjxOLRrPsMBBW4fk1stUgQVifeDrlUG4yMf7lNLrkbmJy+0cl0y\n"
                + "me9l+HMhOPKdLfyebadSy1HlkR8nKSaQW3y3wdcCgYEA8vIL1DWtmaSEx22U0NNR\n"
                + "Zid5vbRxJIYRnGVX75dcwe4XKsgGMJqN2ojVJjOgJpP+d+IY8FHS4IYTfTWXilXG\n"
                + "VL7twVbC9Yw6BS1OAudMbjcEjp4rlEyKTpDf/woyIbr89+3lJQsG77KciBEVPNln\n"
                + "LQL/++Yj8BO9CYPe4FjBkCECgYEAmkKj1YSBHMhVwPDjz8/uPcpwBdunvxqBFw6H\n"
                + "TqfbYAGHOWMQKWk8eX8Y+qy5QNnQfpeMQufYCOw3+zGw6bMAzpKNq+nemQRrccCl\n"
                + "OrlYsMBVGbljqf0/l1iibcG+0uX2L3r1M4ilP99wZpBfS/CkDRSbU3Gc2XWRtm4+\n"
                + "AU7zLUkCgYBFVjQVyvkjNdnpKmFn3NCP5ELDj13dovK/ma75Paw+U9J1+4bIf+0O\n"
                + "hulF6wVUOJKlef7v7S7q4EM7WrbPXpI/Cqyn7B4Q/C0oWL0VCbceUgtvG6qTy8pd\n"
                + "AUxk9W0PnYDK7Sw+jv8iN0zNNr1SqZc37YLy2R1eD0R+3+RQHkl63Q==\n"
                + "-----END RSA PRIVATE KEY-----\n";

            // Expected data has been derived from https://superdry.apphb.com/tools/online-rsa-key-converter
            var rsa = new RSAParameters
            {
                Modulus = Convert.FromBase64String("zw2rOycv+DqPhxJ/XZDsALH0WIn/Yyk97TGKhYME6CuybqlJBVTbpd2th2Fw/bDTwIHXr2wYJubX9b4G0gfVIwemq1rZyyJu2SxoOEK4BQg5s8poQh0Uq5KadP5DVvxk0AkIUySBCvNU+AHxRFJtJ6UZTjtUVYv2Yie+3YWhN3uDZ5kimjB7VSZqgdgnMpfx8kDTtGFHpA1oQNt71z3nyXPRTxZXGlYZlgGNEmv+cd3wYuuQ0LnqZf3thXZfXYd0ASWhfdq0BxGT8WbThk8y6y9/aHsEtaf4QSpc8idVXPu0BwR8P4p5lGzY11a5YERPOCl5BiaHYPgUJxzPnkFipw=="),
                Exponent = Convert.FromBase64String("AQAB"),
                D = Convert.FromBase64String("DboxRQ23D3UKeQEP8qseCnnB4nDbvr5IPcaQEYP7Ok1TWiTdE35pxZhp3dTnAdrMXzH8k8PN4h5h6Qf9OcWNKvJh6XBvihkxUjKwgjJiMus1sCbWPLPi3/ErTmBhJI49PNGsfJ6p84jodDzkuwvf8sXuZJJoZ/eMkTtjWxp18tCd5JSEsyMOSyX6j3Xg/9atX8sZxns7qehndWB7zM8aU9yI9U4Goeq1bPHJPFu3JwhQ9bsYptmNpYzBHAucjlT6FygZCAexFRQkxzCIa5txJx2U4cKeivEzv2yCOeSjhEP4NRkZT+J/+g1FLp2Ei9pYIiu37iUeGkeFkYh3Xq0sIQ=="),
                P = Convert.FromBase64String("+uOWmE91jbACXylQ+GSAchUfAmeqtf2e23TlXzScleEay/elSbf6/HgUhUiUXCa2r+3d6kbEhoKy4Hed1Jm19rYBBIMSLZR0R3Il987x6WAnx+xrkfSWI3Z8fHJoCtTfS/hXA+W/Snr0WlFUMgwaEZzd3FEIU62ZizKgjO1c67E="),
                Q = Convert.FromBase64String("00V5L2a7FGa684RWXxNmFX0ROmAn3Dt9urT8lRsfsRARF52dPqSthS/iLchBIGTBUtZD5EnX5ttu88o5XkjxOLRrPsMBBW4fk1stUgQVifeDrlUG4yMf7lNLrkbmJy+0cl0yme9l+HMhOPKdLfyebadSy1HlkR8nKSaQW3y3wdc="),
                DP = Convert.FromBase64String("8vIL1DWtmaSEx22U0NNRZid5vbRxJIYRnGVX75dcwe4XKsgGMJqN2ojVJjOgJpP+d+IY8FHS4IYTfTWXilXGVL7twVbC9Yw6BS1OAudMbjcEjp4rlEyKTpDf/woyIbr89+3lJQsG77KciBEVPNlnLQL/++Yj8BO9CYPe4FjBkCE="),
                DQ = Convert.FromBase64String("mkKj1YSBHMhVwPDjz8/uPcpwBdunvxqBFw6HTqfbYAGHOWMQKWk8eX8Y+qy5QNnQfpeMQufYCOw3+zGw6bMAzpKNq+nemQRrccClOrlYsMBVGbljqf0/l1iibcG+0uX2L3r1M4ilP99wZpBfS/CkDRSbU3Gc2XWRtm4+AU7zLUk="),
                InverseQ = Convert.FromBase64String("RVY0Fcr5IzXZ6SphZ9zQj+RCw49d3aLyv5mu+T2sPlPSdfuGyH/tDobpResFVDiSpXn+7+0u6uBDO1q2z16SPwqsp+weEPwtKFi9FQm3HlILbxuqk8vKXQFMZPVtD52Ayu0sPo7/IjdMzTa9UqmXN+2C8tkdXg9Eft/kUB5Jet0=")
            };

            using (var stream = new MemoryStream())
            {
                using (var writer = new PemWriter(stream))
                    writer.WritePrivateKey(rsa);

                stream.Seek(0, SeekOrigin.Begin);

                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    var pem = reader.ReadToEnd();
                    Assert.That(pem, Is.EqualTo(expectedPem));
                }
            }
        }
    }
}
