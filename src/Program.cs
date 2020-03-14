using System;
using System.IO;

namespace XSL_Transformer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Please specify the xsl filepath and source xml file path. The output xml will be created in the same folder as source xml");
                Console.WriteLine("XSL Transformer.exe {XSLT File} {XML File}");
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Please specify a valid xsl file");
                return;
            }
            if (!File.Exists(args[1]))
            {
                Console.WriteLine("Please specify a valid source xml file");
                return;
            }
            
            var xslFile = new FileInfo(args[0]);
            var sourceXml = new FileInfo(args[1]);

            try
            {
                var xslProcessor = new XslProcessor(xslFile, sourceXml);
                xslProcessor.Transoform();
                Console.WriteLine("Transformation completed successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occured while transforming! {e.ToString()}");
            }
            
        }
    }
}
