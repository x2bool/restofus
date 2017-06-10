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
                ReadRequestLine(reader, request);
                ReadRequestHeaders(reader, request);
            }

            return request;
        }

        void ReadRequestLine(StreamReader reader, ReactiveRequest request)
        {
            var line = reader.ReadLine();

            if (line != null && line.Length > 0)
            {
                var parts = line.Split(' ');

                if (parts.Length > 0)
                {
                    request.Method = new ReactiveMethod(parts[0]);
                }

                if (parts.Length > 1)
                {
                    request.Url = new ReactiveUrl(parts[1]);
                }
            }
        }

        void ReadRequestHeaders(StreamReader reader, ReactiveRequest request)
        {
            request.Headers = new ReactiveHeaderCollection();

            string line;
            while ((line = reader.ReadLine()) != null && line.Length > 0)
            {
                var parts = line.Split(new[] { ':' }, 2);

                if (parts.Length != 2)
                {
                    throw new FormatException("Invalid HTTP request header");
                }

                var name = parts[0].Trim();
                var val = parts[1].Trim();

                request.Headers.Add(new ReactiveHeader(name, val));
            }
        }

        void ReadRequestBody(StreamReader reader, ReactiveRequest request)
        {
            
        }
    }
}
