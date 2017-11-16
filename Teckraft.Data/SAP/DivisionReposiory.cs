using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.SAP;
using Teckraft.Data.Mappings;

namespace Teckraft.Data.SAP
{
    public class DivisionRepository : IReadableRepository<Division>
    {
         private readonly Mappings.IMappingProvider<Division,Sql.Division> _divisionMapper;
        public DivisionRepository(DivisionMappingProvider divisionMapper)
        {
            this._divisionMapper = divisionMapper;
        }
        public List<Division> ToList
        {
            
            get {
                var list=new List<Division>();
                using (Sql.InitiativeHubFinalEntities dbcontext=new Sql.InitiativeHubFinalEntities()){
                    foreach (var item in dbcontext.Divisions.OrderBy(it=>it.DivisionName)) {
                        list.Add(_divisionMapper.Map(item));
                    }
            }
                return list;
            }
        }

        public ListQueryResult<Division> GetByQuery(ListQuery<Division> query)
        {
            var result=new ListQueryResult<Division>();
                //using (Sql.InitiativeHubFinalEntities dbcontext=new Sql.InitiativeHubFinalEntities()){
                //    foreach (var item in dbcontext.Divisions.Where(it=>  it.DivisionFunctionMappings.Count > 0).OrderBy(it => it.DivisionName))
                //    {
                //        if(result.Items==null)result.Items=new List<Division>();
                //        result.Items.Add(_divisionMapper.Map(item));
                //    }
                //}
            return result;
        }

        public Division GetById(int id)
        {
            using (Sql.InitiativeHubFinalEntities dbcontext = new Sql.InitiativeHubFinalEntities())
            {
                var item = dbcontext.Divisions.FirstOrDefault(it => it.Id == id);
                if (item == null) throw new Exception("Not Found");
                return _divisionMapper.Map(item);
            }
        }

     

        public Division Add(Division item)
        {
            throw new NotImplementedException();
        }

        public Division Update(Division item)
        {
            throw new NotImplementedException();
        }

        public Division Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
