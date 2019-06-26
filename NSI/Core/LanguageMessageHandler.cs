using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace NSI.Api.Core
{
    /// <summary>
    /// Handles assigning culture based on header of the request
    /// </summary>
    public class LanguageMessageHandler : DelegatingHandler
    {
        /// <summary>
        /// Assigns the CulureInfo of the current thread to the one provided in header
        /// </summary>
        /// <param name="request">Request, instance of <see cref="HttpRequestMessage"/></param>
        /// <param name="cancellationToken">Cancellation token, instance of <see cref="CancellationToken"/></param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // 1. prioritize languages based upon quality
            var languages = new List<StringWithQualityHeaderValue>();

            if (request.Headers.AcceptLanguage != null)
            {
                // then check the Accept-Language header.
                languages.AddRange(request.Headers.AcceptLanguage);
            }

            // sort the languages with quality so we can check them in order.
            languages = languages.OrderByDescending(l => l.Quality).ToList();

            CultureInfo culture = null;

            // 2. try to find one language that's available
            foreach (StringWithQualityHeaderValue lang in languages)
            {
                try
                {
                    culture = CultureInfo.GetCultureInfo(lang.Value);
                    break;
                }
                catch (CultureNotFoundException)
                {
                    // ignore the error
                }
            }

            // 3. if a language is available, set the thread culture
            if (culture != null)
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}