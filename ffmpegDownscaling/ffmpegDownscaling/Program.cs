using System.Diagnostics;



    var startInfo = new ProcessStartInfo
    {
        FileName = @"C:\ffmpeg\ffmpeg.exe",
        Arguments = "-y -i vid3.mp4 -an -vf scale=540*380 xxyyyx.mp4",
        WorkingDirectory = @"C:\ffmpeg\",
        CreateNoWindow = true,
        UseShellExecute = false,

    };

    using (var process = new Process { StartInfo = startInfo })
    {
        process.Start();
        process.WaitForExit();

    }
