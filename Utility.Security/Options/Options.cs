using System.Collections.Generic;
using CommandLine.Text;

namespace Utility.Security.Options
{
    public class Options : IOptions
    {
        [Usage(ApplicationAlias = "Utility.Security")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Encrypt scenario", new EncryptOptions { PlainString = "some text to encrypt", PassPhrase = "A pass phrase used to encrypt the plain string" });
                yield return new Example("Encrypt scenario", new DecryptOptions { CipherString = "some cipher to decrypt", PassPhrase = "A pass phrase used to decrypt the cipher string" });
            }
        }
    }
}