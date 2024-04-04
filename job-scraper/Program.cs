using HtmlAgilityPack;

var site = @"https://www.seek.com.au/software-jobs/in-Sydney-NSW-2000?&salaryrange=100000-&salarytype=annual&page=";

HtmlWeb web = new HtmlWeb();
Dictionary<String, JobListing> jobs = new Dictionary<string, JobListing>();
int dupeCount = 0;

// Get the first N pages of results
for (int i = 1; i < 6; i++)
{
    var htmlDoc = web.Load(site + i);
    var nodes = htmlDoc.DocumentNode.SelectNodes("//article[@data-card-type='JobCard']");
    foreach (var node in nodes)
    {
        string id = node.Attributes["data-job-id"].Value;
        string jobTitle = node.Attributes["aria-label"].Value;

        // We get back a set of nodes as an iterator and need to skip the first few entries to get our location and company 
        // This is brittle
        var subNode = node.Descendants("a").GetEnumerator();
        subNode.MoveNext();
        subNode.MoveNext();
        subNode.MoveNext();
        string company = subNode.Current.InnerText;
        subNode.MoveNext();
        string location = subNode.Current.InnerText;
        string link = @"https://www.seek.com.au/job/" + id;
        var job = new JobListing(id, jobTitle, company, link, location);

        try
        {
            jobs.Add(id, job);

        }
        catch (System.ArgumentException)
        {
            Console.WriteLine("Dupe job listing: {0}", jobs[id].DisplayAttributes());
            dupeCount++;
        }
    }

}
foreach (var job in jobs)
{
    Console.WriteLine(job.Value.DisplayAttributes());
}

Console.WriteLine("Total Jobs Indexed: {0}. Total duplicates: {1}", jobs.Count, dupeCount);

