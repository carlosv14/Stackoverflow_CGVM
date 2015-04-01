using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Portable;
using RestSharp.Portable.Deserializers;

namespace Stackoverflow_CGVM.Phone
{
    class Stackoverflow_CGVMAPI
    {
        private RestClient cliente;
        public Stackoverflow_CGVMAPI()
        {
            cliente = new RestClient()
            {
                BaseUrl = new Uri("http://localhost:1885/")
            };
        }

        public IEnumerable<QuestionListModel> GetQuestionListModels()
        {
            RestRequest request = new RestRequest{Resource = "api/QuestionIndex"};
            var result = cliente.Execute(request);
            RestSharp.Portable.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
            var list = deserial.Deserialize<IEnumerable<QuestionListModel>>(result.Result);
            return list;
        }
    }

    }

