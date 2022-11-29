namespace InternetERP.Data.Models
{
    using InternetERP.Data.Common.Models;

    public class FailurePhase : BaseDeletableModel<int>
    {
        public int FailureId { get; set; }

        public Failure Failure { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int FailureTeamId { get; set; }

        public FailureTeam FailureTeam { get; set; }

        public int StatusFailureId { get; set; }

        public StatusFailure StatusFailure { get; set; }

        public string Note { get; set; }
    }
}
