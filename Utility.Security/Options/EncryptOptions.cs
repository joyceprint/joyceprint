using CommandLine;

namespace Utility.Security.Options
{
    [Verb("encrypt", HelpText = "Encrypt a plain string using a pass phrase")]
    public class EncryptOptions : Options
    {
        [Option('s', "plainString", Required = true, HelpText = "Input string to encrypt")]
        public string PlainString { get; set; }

        [Option('p', "passPhrase", Required = true, HelpText = "Phrase used to encrypt the plain string")]
        public string PassPhrase { get; set; }
    }
}