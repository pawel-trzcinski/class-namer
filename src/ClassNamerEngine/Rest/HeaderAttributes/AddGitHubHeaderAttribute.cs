﻿namespace ClassNamerEngine.Rest.HeaderAttributes
{
    /// <summary>
    /// Attribute for adding GitHub repo attribute.
    /// </summary>
    public class AddGitHubHeaderAttribute : AddHeaderAttribute
    {
        /// <summary>
        /// GitHub header name.
        /// </summary>
        public const string GIT_HUB = "GitHub";

        /// <summary>
        /// GitHub header value.
        /// </summary>
        public const string GIT_HUB_ADDRESS = "https://github.com/pawel-trzcinski/class-namer";

        /// <summary>
        /// Initializes a new instance of the <see cref="AddGitHubHeaderAttribute"/> class.
        /// </summary>
        public AddGitHubHeaderAttribute()
            : base(GIT_HUB, GIT_HUB_ADDRESS)
        {
        }
    }
}