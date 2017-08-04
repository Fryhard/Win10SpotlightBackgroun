using ImageProcessor;
using Quartz;
using System;
using System.Configuration;
using System.IO;

namespace Windows10SpotlightBackgrounds
{
    [DisallowConcurrentExecution]
    class ImageCopyJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                string sourcePath = ConfigurationManager.AppSettings["sourcePath"];
                string destimationPath = ConfigurationManager.AppSettings["destimationPath"];

                DirectoryInfo di = new DirectoryInfo(sourcePath);
                foreach (FileInfo fi in di.GetFiles())
                {
                    try
                    {
                        bool copy = false;
                        using (MemoryStream inputStream = new MemoryStream(File.ReadAllBytes(fi.FullName)))
                        {
                            if (inputStream.Length > 0)
                            {
                                using (ImageFactory imageFactory = new ImageFactory(true))
                                {
                                    imageFactory.Load(inputStream);
                                    if ((imageFactory.Image.Width >= 1920) &&
                                        (imageFactory.Image.Height >= 1080))
                                    {
                                        copy = true;
                                    }
                                }
                            }
                        }

                        if (copy)
                        {
                            string dest = Path.Combine(destimationPath, fi.Name + ".jpg");
                            if (!File.Exists(dest))
                            {
                                fi.CopyTo(dest);
                            }
                        }
                    }
                    catch (Exception inEx)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
