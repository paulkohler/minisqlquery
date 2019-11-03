#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion

using System;

namespace MiniSqlQuery.Core
{
    /// <summary>
    /// 	A simple text finding service. Currently supports forward only text matching.
    /// </summary>
    public class BasicTextFindService : ITextFindService
    {
        /// <summary>
        /// 	The services reference.
        /// </summary>
        private readonly IApplicationServices _services;

        /// <summary>
        /// 	Initializes a new instance of the <see cref = "BasicTextFindService" /> class. Creates a new text find service.
        /// </summary>
        /// <param name = "applicationServices">A reference to the application services.</param>
        public BasicTextFindService(IApplicationServices applicationServices)
        {
            _services = applicationServices;
        }

        /// <summary>
        /// 	Looks for the next match depending on the settings in the <paramref name = "request" />.
        /// </summary>
        /// <param name = "request">The text find request.</param>
        /// <returns>An updated request with the relevent values adjusted (namely position).</returns>
        public FindTextRequest FindNext(FindTextRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            /*
			if (request.SearchUp)
			{
			    // todo - I think its the TextProvider's job...?
			}
			else // search down.
			{
			int pos = request.TextProvider.FindString(request.SearchValue, request.Position, request.StringComparison);
			    //pos = request.TextProvider.FindString(request);
			}
			*/
            int pos = request.TextProvider.FindString(request.SearchValue, request.Position, request.StringComparison);

            if (pos > -1)
            {
                // the editor will highlight the find
                request.Position = pos + request.SearchValue.Length;
            }
            else
            {
                // todo - notify, beep etc

                // reset to start of buffer.
                request.Position = 0;
            }

            return request;
        }
    }
}