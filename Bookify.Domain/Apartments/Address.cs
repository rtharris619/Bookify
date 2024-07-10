using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Apartments;

public record Address(string Country, string Province, string PostalCode, string City, string Street);
