using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Xsl;

namespace XSL_Transformer
{
    internal class XslProcessor
    {
        private readonly FileInfo _xslFile;
        private readonly FileInfo _soureXml;
        private FileInfo _outputXml;

        public XslProcessor(FileInfo xslFile, FileInfo sourceXml)
        {
            _xslFile = xslFile;
            _soureXml = sourceXml;
        }

        public void Transoform()
        {
            if (!PrepareOutputXml())
            {
                Console.WriteLine("Unable to prepare output xml path. Possibly invalid source xml path.");
                return;
            }

            var transformer = new XslCompiledTransform();
            var sw = new Stopwatch();
            
            sw.Start();
            transformer.Load(_xslFile.FullName);
            Console.WriteLine("Time to load Xsl {0}", sw.Elapsed);
            
            sw.Restart();
            transformer.Transform(_soureXml.FullName, _outputXml.FullName);
            Console.WriteLine("Time to transform source xml {0}", sw.Elapsed);
            
            Console.WriteLine("Succesfully written output file at {0}", _outputXml.FullName);
        }

        private bool PrepareOutputXml()
        {
            if (_soureXml.Exists)
            {
                var outputFilePath = Path.Combine(_soureXml.DirectoryName, $"{Path.GetFileNameWithoutExtension(_soureXml.Name)}_transformed_{DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss")}.{_soureXml.Extension}");
                _outputXml = new FileInfo(outputFilePath);
                return true;
            }
            return false;
        }
    }
}
