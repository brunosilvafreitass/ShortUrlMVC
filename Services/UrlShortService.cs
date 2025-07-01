namespace ShortUrlMvc.Services
{
    public class UrlShortService
    {
        public string IdToShortURL(int n)
        {
            // Map to store 62 possible characters 
            char[] map = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

            String shorturl = "";

            // Convert given integer id to a base 62 number 
            while (n > 0)
            {
                // use above map to store actual character 
                // in short url 
                shorturl += map[n % 62];
                n = n / 62;
            }

            // Reverse shortURL to complete base conversion 
            return Reverse(shorturl);
        }
        static string Reverse(String input)
        {
            char[] a = input.ToCharArray();
            int l, r = a.Length - 1;
            for (l = 0; l < r; l++, r--)
            {
                char temp = a[l];
                a[l] = a[r];
                a[r] = temp;
            }
            return String.Join("", a);
        }
    }
}