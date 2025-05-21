using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AdoptMe
{
    public class AdoptionRequest
    {
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public int AnimalId { get; set; }
        public string Information { get; set; }
        public int? ProcessedBy { get; set; }
        public string RequestStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }

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
            // Get the animalId and userId for this request
            string selectQuery = "SELECT animal_id, user_id FROM AdoptionRequest WHERE request_id = @requestId";
            SqlParameter[] selectParams = { new SqlParameter("@requestId", requestId) };
            int animalId = 0;
            int userId = 0;
            using (var reader = DatabaseConnection.ExecuteReader(selectQuery, selectParams))
            {
                if (reader.Read())
                {
                    animalId = reader.GetInt32(0);
                    userId = reader.GetInt32(1);
                }
            }

            string query = @"UPDATE AdoptionRequest
                     SET request_status = @status, 
                         processed_by = @adminId,
                         processed_at = @processedAt
                     WHERE request_id = @requestId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@status", status),
                new SqlParameter("@adminId", adminId),
                new SqlParameter("@processedAt", DateTime.Now),
                new SqlParameter("@requestId", requestId)
            };

            DatabaseConnection.ExecuteNonQuery(query, parameters);

            if (status == "Approved")
            {
                Animal.UpdateStatus(animalId, "adopted", userId);
            }
            else if (status == "Deny")
            {
                Animal.UpdateStatus(animalId, "not_adopted", null);
            }
        }



        public static List<AdoptionRequest> GetAllRequests()
        {
            var requests = new List<AdoptionRequest>();
            string query = @"SELECT request_id, user_id, animal_id, information, processed_by, request_status, created_at, processed_at FROM AdoptionRequest";
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
                    request.ProcessedAt = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7); // <-- handle nullable
                    requests.Add(request);
                }
            }
            return requests;
        }
        public static List<AdoptionRequest> GetCurrentUserRequests(int userId)
        {
            var requests = new List<AdoptionRequest>();
            string query = @"SELECT request_id, user_id, animal_id, information, processed_by, request_status, created_at, processed_at 
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
                    request.ProcessedAt = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7);
                    requests.Add(request);
                }
            }
            return requests;
        }

        public static DataTable GetAdoptionRequestsWithNames(int userId, int requestId)
        {
            string query = @"
                SELECT 
                    ar.request_id,
                    ar.user_id,
                    ar.animal_id,
                    ar.information,
                    ar.processed_by,
                    ar.request_status,
                    ar.created_at,
                    ar.processed_at,
                    a_admin.name AS admin_name,
                    adoptee.name AS adoptee_name,
                    animal.name AS animal_name
                FROM AdoptionRequest ar
                LEFT JOIN Admin a_admin ON ar.processed_by = a_admin.admin_id
                LEFT JOIN Adoptee adoptee ON ar.user_id = adoptee.adoptee_id
                LEFT JOIN Animal animal ON ar.animal_id = animal.animal_id
                WHERE ar.user_id = @userId AND ar.request_id = @requestId
            ";
            SqlParameter[] parameters = {
        new SqlParameter("@userId", userId),
        new SqlParameter("@requestId", requestId)
    };
            return DatabaseConnection.ExecuteDataTable(query, parameters);
        }


    }
}

