using System;
using System.IO;
using System.Linq;
using DevExpressGroupingExample.Dto;
using DevExpressGroupingExample.Persistence;
using DevExtreme.AspNet.Data;

namespace DevExpressGroupingExample
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new AppDbContext();

            var result = db.Persons
                .Select(x => new ActivityParticipantDto
                {
                    ActivityName = x.Activity.Name,
                    FirstName = x.Participant.FirstName,
                    LastName = x.Participant.LastName,
                    Status = x.ResultStatus,
                    GroupCategoryTypes = x.Participant.ParticipantGroups
                        .Select(y => new GroupCategoryType
                        {
                            GroupCategory = y.GroupCategory,
                            GroupFullName = y.GroupName
                        })
                });

            var data = DataSourceLoader.Load(result, CreateDataSourceLoadOptions());
            Console.WriteLine($"Load Result: TotalCount [{data.totalCount}] GroupCount [{data.groupCount}]");
        }

        static DataSourceLoadOptions CreateDataSourceLoadOptions()
        {
            return new DataSourceLoadOptions
            {
                Group = new[] { new GroupingInfo { Desc = false, IsExpanded = false, Selector = "status" }},
                RequireTotalCount = true,
                RequireGroupCount = true
            };
        }
    }
}