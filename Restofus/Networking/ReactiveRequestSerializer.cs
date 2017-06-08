using Restofus.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Restofus.Networking
{
    public class ReactiveRequestSerializer
    {
        public ReactiveRequest Deserialize(FileInfo file)
        {
            var request = new ReactiveRequest();

            using (var reader = file.OpenText())
            {
                ReadFileHeader(reader, request);

                ReadRequestLine(reader, request);
                //ReadRequestHeaders(reader, request);
                //ReadRequestBody(reader, request);
            }

            return request;
        }

        void ReadEmptyLine(StreamReader reader)
        {
            var line = reader.ReadLine();

            if (line.Length > 0)
            {
                throw new FormatException("Empty line was expected");
            }
        }

        void ReadFileHeader(StreamReader reader, ReactiveRequest request)
        {
            var line = reader.ReadLine();
            
            var parts = line.Split(' ');

            if (parts.Length < 2)
            {
                throw new FormatException("Invalid file header");
            }

            if (parts[0] != ".rst")
            {
                throw new FormatException("Invalid file header");
            }

            ShortGuid guid;
            if (!ShortGuid.TryParse(parts[1], out guid))
            {
                throw new FormatException("Invalid file header");
            }

            request.Id = (Guid)guid;

            ReadEmptyLine(reader);
        }

        void ReadRequestLine(StreamReader reader, ReactiveRequest request)
        {
            var line = reader.ReadLine();
            var parts = line.Split(' ');

            if (parts.Length != 3)
            {
                throw new FormatException("Invalid HTTP request line");
            }

            request.Method = new ReactiveMethod(parts[0]);
            request.Url = new ReactiveUrl(parts[1]);
        }

        void ReadRequestHeaders(StreamReader reader, ReactiveRequest request)
        {
            
        }

        void ReadRequestBody(StreamReader reader, ReactiveRequest request)
        {
            
        }
    }
}
