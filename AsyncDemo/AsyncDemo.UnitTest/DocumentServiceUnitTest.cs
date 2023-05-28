namespace AsyncDemo.UnitTest
{
    public class DocumentServiceUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(@"C:\Users\User\Test\Test1.txt", "Hello, world")]
        public void test_create_document(string filePath, string content)
        {
            DocumentService.Create(filePath, content);

            var actural = File.ReadAllText(filePath);
            Assert.That(actural, Is.EqualTo(content));
        }


        [Test]
        public async Task test_create_document_async()
        {
            var iRepeating = 5;
            var sFilePath = @"C:\Users\User\Test\Test{0}.txt";
            var sContent = @"Hello, welcome to my world {0}!";

            // delete existing files
            for (int idx = 0; idx < iRepeating; idx++)
            {
                if(File.Exists(String.Format(sFilePath, idx + 1)))
                {
                    File.Delete(sFilePath);
                }
            }

            // start async call paralle
            var tasks = new List<Task>();
            for(int idx = 0; idx<iRepeating; idx++)
            {
                var task = DocumentService.CreateAsync(String.Format(sFilePath, idx + 1), String.Format(sContent, idx + 1));
                tasks.Add(task);
            }
            await Task.WhenAll(tasks.ToArray());
            // end of async call

            for (int idx = 0; idx < iRepeating; idx++)
            {
                if (!File.Exists(String.Format(sFilePath, idx + 1)))
                {
                    Assert.Fail("File doesn't get created: " + String.Format(sFilePath, idx + 1));
                }
                var actural = File.ReadAllText(String.Format(sFilePath, idx + 1));
                Assert.That(actural, Is.EqualTo(String.Format(sContent, idx + 1)));
            }
        }
    }
}