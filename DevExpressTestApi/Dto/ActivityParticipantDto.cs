using System.Collections.Generic;

namespace DevExpressGroupingExample.Dto
{
    public class ActivityParticipantDto
    {
        public string ActivityName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        public IEnumerable<GroupCategoryType> GroupCategoryTypes { get; set; }
    }
}