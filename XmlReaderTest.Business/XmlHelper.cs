using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace XmlReaderTest.Business
{
    public class XmlHelper
    {
        public TResponse GetXmlResponse<TRequest, TResponse>(TRequest request, string url, HttpMethod httpMethod, Encoding encoding, Dictionary<string, string>? customHeaders, int timeOut = 0)
            where TRequest : class where TResponse : class, new()
        {
            TResponse? response = null;
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    if (timeOut > 0)
                    {
                        httpClient.Timeout = TimeSpan.FromSeconds(timeOut);
                    }
                    if (customHeaders != null && customHeaders.Any())
                    {
                        httpClient.DefaultRequestHeaders.Clear();
                        foreach (var item in customHeaders)
                        {
                            httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                        }
                    }
                    HttpRequestMessage requestMessage = new HttpRequestMessage(httpMethod, url);
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(TRequest));
                    using (MemoryStream writer = new MemoryStream())
                    {
                        XmlWriterSettings settings = new XmlWriterSettings();
                        settings.Encoding = encoding;
                        settings.Indent = false;
                        using (XmlWriter xmlWriter = XmlWriter.Create(writer, settings))
                        {
                            xmlSerializer.Serialize(xmlWriter, request);
                            string xml = encoding.GetString(writer.ToArray());
                            requestMessage.Content = new StringContent(xml, encoding, "text/xml");
                        }
                    }

                    var xmlResponse = httpClient.Send(requestMessage);
                    if (xmlResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        using (StringReader strReader = new StringReader(xmlResponse.Content.ReadAsStringAsync().Result))
                        {
                            XmlSerializer xmlDeSerializer = new XmlSerializer(typeof(TResponse));
                            response = xmlDeSerializer.Deserialize(strReader) as TResponse;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Add Your Log Method here
                throw;
            }
#pragma warning disable CS8603 // Possible null reference return.
            return response;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public TResponse GetXmlResponseWithProxy<TRequest, TResponse>(TRequest request, string url, HttpMethod httpMethod, Encoding encoding, HttpClientHandler clientHandler, Dictionary<string, string>? customHeaders, int timeOut = 0)
            where TRequest : class where TResponse : class, new()
        {
            TResponse? response = null;
            try
            {
                if(object.Equals(clientHandler,null))
                {
                    throw new ArgumentNullException("Client Handler can not be null!!!");
                }

                using (HttpClient httpClient = new HttpClient(clientHandler))
                {
                    if (timeOut > 0)
                    {
                        httpClient.Timeout = TimeSpan.FromSeconds(timeOut);
                    }
                    if (customHeaders != null && customHeaders.Any())
                    {
                        httpClient.DefaultRequestHeaders.Clear();
                        foreach (var item in customHeaders)
                        {
                            httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                        }
                    }
                    HttpRequestMessage requestMessage = new HttpRequestMessage(httpMethod, url);
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(TRequest));
                    using (MemoryStream writer = new MemoryStream())
                    {
                        XmlWriterSettings settings = new XmlWriterSettings();
                        settings.Encoding = encoding;
                        settings.Indent = false;
                        using (XmlWriter xmlWriter = XmlWriter.Create(writer, settings))
                        {
                            xmlSerializer.Serialize(xmlWriter, request);
                            string xml = encoding.GetString(writer.ToArray());
                            requestMessage.Content = new StringContent(xml, encoding, "text/xml");
                        }
                    }

                    var xmlResponse = httpClient.Send(requestMessage);
                    if (xmlResponse.StatusCode == HttpStatusCode.OK)
                    {
                        using (StringReader strReader = new StringReader(xmlResponse.Content.ReadAsStringAsync().Result))
                        {
                            XmlSerializer xmlDeSerializer = new XmlSerializer(typeof(TResponse));
                            response = xmlDeSerializer.Deserialize(strReader) as TResponse;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Add Your Log Method here
                throw;
            }
#pragma warning disable CS8603 // Possible null reference return.
            return response;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}