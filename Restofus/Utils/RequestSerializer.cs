using Restofus.Networking;
using Restofus.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Restofus.Utils
{
    public class RequestSerializer
    {
        public async Task<ReactiveRequest> Deserialize(FileInfo file)
        {
            var request = new ReactiveRequest();

            using (var reader = file.OpenText())
            {
                await ReadRequestLine(reader, request);
                await ReadRequestHeaders(reader, request);
            }

            return request;
        }

        async Task ReadRequestLine(StreamReader reader, ReactiveRequest request)
        {
            var line = await reader.ReadLineAsync();

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

        async Task ReadRequestHeaders(StreamReader reader, ReactiveRequest request)
        {
            request.Headers = new ReactiveHeaderCollection();

            string line;
            while ((line = await reader.ReadLineAsync()) != null && line.Length > 0)
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
    }
}
