using Microsoft.Playwright;
using System.Drawing;
namespace appform.Models.UI.POM
{
    public class FormSubmissionsPage : PageBase
    {
        public override string Title => "Form Submissions";

        public FormSubmissionsPage(IPage page) : base(page)
        {
        }

        public async Task<bool> IsHeaderDisplayed()
        {
            var element = await Page.QuerySelectorAsync("//h1[text()='Successful Form Submissions']");
            return await element.IsVisibleAsync();
        }

        public async Task<string> GetName()
        {
            var result = await GetNameOrEmailValueByLabel(label: "Name");
            return result;
        }

        public async Task<string> GetEmail()
        {
            var result = await GetNameOrEmailValueByLabel(label: "Email");

            return result;
        }

        private async Task<string> GetNameOrEmailValueByLabel(string label)
        {
            var result = await Page.EvaluateAsync<string>($@"
            (label) => {{
                const xpath = `//strong[text()='{label}:']/following-sibling::text()[1]`;
                const element = document.evaluate(xpath, document, null, XPathResult.STRING_TYPE, null).stringValue;
                return element.trim();
            }}", label
            );

            return result;
        }

        public async Task<bool> IsAvatarMatched(string pathToExpectedAvatar)
        {
            var element = await Page.QuerySelectorAsync("//img[@alt='Avatar']");
            var hrefAttributeValue = await element.GetAttributeAsync("src");
            var image_url = $"{Page.Url}{hrefAttributeValue}".Replace("/success", "");
            var pathToActualAvatar = await DownloadAvatar(image_url);
            var result = await CompareImagesAsync(pathToExpectedAvatar, pathToActualAvatar);

            return result;
        }

        private async Task<string> DownloadAvatar(string image_url)
        {
            var actualAvatarPath = Path.Combine(Path.GetFullPath(@"..\..\..\..\"), "appform", "downloads");

            var result = string.Empty;
            using (var client = new System.Net.Http.HttpClient())
            {
                var imageBytes = await client.GetByteArrayAsync(image_url);

                if (!Path.Exists(actualAvatarPath))
                {
                    Directory.CreateDirectory(actualAvatarPath);
                }
                result = Path.Combine(actualAvatarPath, $"{Path.GetRandomFileName()}.jpg");
                File.WriteAllBytes(result, imageBytes);
            }


            return result;
        }

        public static async Task<bool> CompareImagesAsync(string imagePath1, string imagePath2)
        {
            // Load images asynchronously
            Bitmap image1 = await Task.Run(() => new Bitmap(imagePath1));
            Bitmap image2 = await Task.Run(() => new Bitmap(imagePath2));

            // Check if dimensions are the same
            if (image1.Width != image2.Width || image1.Height != image2.Height)
            {
                return false;
            }

            // Compare pixel by pixel
            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    Color pixel1 = image1.GetPixel(x, y);
                    Color pixel2 = image2.GetPixel(x, y);

                    if (pixel1 != pixel2)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
