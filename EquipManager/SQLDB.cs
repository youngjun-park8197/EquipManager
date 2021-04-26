using jsLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquipManager
{
    public class SQLDB
    {
        // 클래스 변수 정의        
        SqlConnection sqlConn = new SqlConnection();
        SqlCommand sqlCmd = new SqlCommand();
        string connStr;

        // 클래스 생성자 정의 - 클래스명과 동일
        // ex) SQLDB() { ... }
        public SQLDB(string s) // 디폴트 값 X, SQLDB 생성 시 반드시 s가 필요(connection string 연결 문자열)
        {
            connStr = s;
            sqlConn.ConnectionString = connStr; // connection string에 의해 연결 설정 
            sqlConn.Open();
            sqlCmd.Connection = sqlConn;
        }

        // 클래스 함수(메소드) 정의
        // ex) RunSql(...) { ... }
        public object RunSql(string sql) // "select count(*) from fStatus
        {
            try
            {
                sqlCmd.CommandText = sql;
                if (jslib.GetToken(0, sql.Trim(), ' ').ToUpper() == "SELECT")
                {
                    SqlDataReader sr = sqlCmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(sr);
                    sr.Close();

                    return dt;
                }
                else
                {
                    return sqlCmd.ExecuteNonQuery(); // insert, update, delete 등 조회 결과가 없는 sql문 처리
                }
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.Message);
                return null;
            }
        }

        
        public object Get(string sql) // 단일 필드 데이터 반환 
        {
            try
            {
                sqlCmd.CommandText = sql;
                if (jslib.GetToken(0, sql.Trim(), ' ').ToUpper() == "SELECT")
                {
                    return sqlCmd.ExecuteScalar();
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
                return null;
        }

    }
}
