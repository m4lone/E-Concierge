using qodeless.messaging.Worker.ViewModels.Common;
using System;
using System.Collections.Generic;

namespace qodeless.messaging.Worker.ViewModels.Response
{
    public class PlayResponse : BaseResponse
    {
        public List<StatusResponse> StatusResponses { get; set; }
    }

    public class StatusResponse
    {
        public StatusResponse(int rowId, bool success)
        {
            RowId = rowId;
            Success = success;
        }
        public int RowId { get; set; }
        public bool Success { get; set; }
    }
}
