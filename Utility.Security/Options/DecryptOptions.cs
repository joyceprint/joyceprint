using CommandLine;

namespace Utility.Security.Options
{
    [Verb("decrypt", HelpText = "Decrypt a cipher string using a pass phrase")]
    public class DecryptOptions : Options
    {
        [Option('c', "cipherString", Required = true, HelpText = "Input cipher to decrypt")]
        public string CipherString { get; set; }

        [Option('p', "passPhrase", Required = true, HelpText = "Phrase used to encrypt the plain string")]
        public string PassPhrase { get; set; }
    }
}