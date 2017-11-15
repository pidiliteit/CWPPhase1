using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Settings;
using Teckraft.Data.Mappings;

namespace Teckraft.Data.Transaction
{
    public class DepartmentRepository : IReadableRepository<Department>
    {
        private readonly Mappings.IMappingProvider<Department, Sql.Division> _DepartmentMapper;
        public DepartmentRepository(DepartmentMappingProvider _DepartmentMapper)
        {
            this._DepartmentMapper = _DepartmentMapper;
        }


        public List<Department> ToList
        {
            get
            {
                var list = new List<Department>();
                using (Sql.InitiativeHubFinalEntities dbcontext = new Sql.InitiativeHubFinalEntities())
                {
                    foreach (var item in dbcontext.Divisions.OrderBy(it => it.DivisionName))
                    {
                        list.Add(_DepartmentMapper.Map(item));
                    }
                }
                return list;
            }
        }

        public ListQueryResult<Department> GetByQuery(ListQuery<Department> query)
        {
            var result = new ListQueryResult<Department>();
            using (Sql.InitiativeHubFinalEntities dbcontext = new Sql.InitiativeHubFinalEntities())
            {
                if (query.Parameters != null)
                {
                    foreach (var p in query.Parameters)
                    {
                        if (p.Name == "depName")
                        {
                            foreach (var item in dbcontext.Divisions.Where(o=> o.DivisionName==p.Value).OrderBy(it => it.DivisionName))
                            {
                                if (result.Items == null) result.Items = new List<Department>();
                                result.Items.Add(_DepartmentMapper.Map(item));
                            }
                        }
                        else if (p.Name == "DealerDivision")
                        {

                            foreach (var item in dbcontext.Divisions.Where(o => o.DivisionName == "CC" || o.DivisionName == "COMMON-RURBAN/EXPORT" || o.DivisionName == "CP - ASF" || o.DivisionName == "CP - MNT" || o.DivisionName == "FV").OrderBy(it => it.DivisionName))
                            {
                                if (result.Items == null) result.Items = new List<Department>();
                                result.Items.Add(_DepartmentMapper.Map(item));
                            }
                        }
                    }
                }
                else
                {
                    foreach (var item in dbcontext.Divisions.OrderBy(it => it.DivisionName))
                    {
                        if (result.Items == null) result.Items = new List<Department>();
                        result.Items.Add(_DepartmentMapper.Map(item));
                    }
                }
            }
            return result;
        }

        public Department GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
