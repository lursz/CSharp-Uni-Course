namespace lab3
{
    public class Tweet
    {
        public String? Text { get; set; }
        public String? UserName { get; set; }
        public String? LinkToTweet { get; set; }
        public String? FirstLinkUrl { get; set; }
        public String? CreatedAt { get; set; }
        public String? TweetEmbedCode { get; set; }

        public Tweet()
        {
        }
        public Tweet(String? text, String? userName, String? linkToTweet, String? firstLinkUrl, String? createdAt, String? tweetEmbedCode)
        {
            Text = text;
            UserName = userName;
            LinkToTweet = linkToTweet;
            FirstLinkUrl = firstLinkUrl;
            CreatedAt = createdAt;
            TweetEmbedCode = tweetEmbedCode;
        }

        public override string ToString()
        {
            return $"Text: {Text}, UserName: {UserName}, LinkToTweet: {LinkToTweet}, FirstLinkUrl: {FirstLinkUrl}, CreatedAt: {CreatedAt}, TweetEmbedCode: {TweetEmbedCode}";
        }
    }


}