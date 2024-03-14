using System.ComponentModel;
using System.Runtime.Serialization;

namespace JokesAPI.DTOs
{
    public class ReactToJokeDto
    {
        /// <summary>
        /// Action enum.
        /// </summary>
        [DataContract]
        public enum ACTION_CHOICES
        {
            /// <summary>
            /// None.
            /// </summary>
            [Description("Invalid Option")]
            [EnumMember]
            NONE,
            /// <summary>
            /// LIKE.
            /// </summary>
            [Description("LIKE")]
            [EnumMember]
            LIKE,
            /// <summary>
            /// DISLIKE.
            /// </summary>
            [Description("DISLIKE")]
            [EnumMember]
            DISLIKE
        }

        /// <summary>
        /// Action, Like or Dislike a Joke.
        /// </summary>
        public required ACTION_CHOICES Action { get; set; }
    }
}
