using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AdoptMe
{
    internal class AdoptionRequest
    {
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public int AnimalId { get; set; }
        public string Information { get; set; }
        public int? ProcessedBy { get; set; }
        public string RequestStatus { get; set; }
        public DateTime CreatedAt { get; set; }

        public AdoptionRequest(int userId, int animalId, string information, string requestStatus = "Pending", int? processedBy = null)
        {
            UserId = userId;
            AnimalId = animalId;
            Information = information;
            RequestStatus = requestStatus;
            ProcessedBy = processedBy;
            CreatedAt = DateTime.Now;
        }

        public void SaveToDatabase()
        {
            string query = @"INSERT INTO AdoptionRequest 
            (user_id, animal_id, information, processed_by, request_status)
            VALUES (@user_id, @animal_id, @information, @processed_by, @request_status)";

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@user_id", UserId),
            new SqlParameter("@animal_id", AnimalId),
            new SqlParameter("@information", Information),
            new SqlParameter("@processed_by", (object)ProcessedBy ?? DBNull.Value),
            new SqlParameter("@request_status", RequestStatus)
            };

            DatabaseConnection.ExecuteNonQuery(query, parameters);
        }
        // Approve the request
        public static void ApproveRequest(int requestId, int adminId)
        {
            UpdateRequestStatus(requestId, "Approved", adminId);
        }

        // Deny the request
        public static void DenyRequest(int requestId, int adminId)
        {
            UpdateRequestStatus(requestId, "Deny", adminId);
        }

        // Helper method to update status and processed_by
        private static void UpdateRequestStatus(int requestId, string status, int adminId)
        {
            string query = @"UPDATE AdoptionRequest
                             SET request_status = @status, processed_by = @adminId
                             WHERE request_id = @requestId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@status", status),
                new SqlParameter("@adminId", adminId),
                new SqlParameter("@requestId", requestId)
            };

            DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        public static List<AdoptionRequest> GetAllRequests()
        {
            var requests = new List<AdoptionRequest>();
            string query = @"SELECT request_id, user_id, animal_id, information, processed_by, request_status, created_at FROM AdoptionRequest";
            using (SqlDataReader reader = DatabaseConnection.ExecuteReader(query, new SqlParameter[] { }))
            {
                while (reader.Read())
                {
                    var request = new AdoptionRequest(
                        userId: reader.GetInt32(1),
                        animalId: reader.GetInt32(2),
                        information: reader.GetString(3),
                        requestStatus: reader.GetString(5),
                        processedBy: reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4)
                    );
                    request.RequestId = reader.GetInt32(0);
                    request.CreatedAt = reader.GetDateTime(6);
                    requests.Add(request);
                }
            }
            return requests;
        }
        public static List<AdoptionRequest> GetCurrentUserRequests(int userId)
        {
            var requests = new List<AdoptionRequest>();
            string query = @"SELECT request_id, user_id, animal_id, information, processed_by, request_status, created_at 
                     FROM AdoptionRequest
                     WHERE user_id = @user_id";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@user_id", userId)
            };
            using (SqlDataReader reader = DatabaseConnection.ExecuteReader(query, parameters))
            {
                while (reader.Read())
                {
                    var request = new AdoptionRequest(
                        userId: reader.GetInt32(1),
                        animalId: reader.GetInt32(2),
                        information: reader.GetString(3),
                        requestStatus: reader.GetString(5),
                        processedBy: reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4)
                    );
                    request.RequestId = reader.GetInt32(0);
                    request.CreatedAt = reader.GetDateTime(6);
                    requests.Add(request);
                }
            }
            return requests;
        }



    }
}

