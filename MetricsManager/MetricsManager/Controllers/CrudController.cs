using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace MetricsManager.Controllers
{
    [Route("api")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly ValuesHolder holder;

        public CrudController(ValuesHolder holder)
        {
            this.holder = holder;
        }

        [HttpPost("set")]
        public IActionResult Create([FromQuery] DateTime? date, [FromQuery] int? temperature)
        {
            DataAndTemp dt = new DataAndTemp();

            if(date != null)
            {
                dt.Date = (DateTime)date;
            }
            else
            {
                dt.Date = DateTime.Now;
            }

            if (temperature != null)
            {
                dt.Temperature = (int)temperature;
                holder.Values.Add(dt); // without temp is nothing to save
            }

            return Ok();
        }

        [HttpGet("get")]
        public IActionResult Read([FromQuery] DateTime? dateStart, [FromQuery] DateTime? dateEnd)
        {
            if (dateStart != null)
            {
                System.Collections.Generic.IEnumerable<DataAndTemp> result = null;
                if (dateEnd != null) // with dateEnd = return range
                {
                    result = holder.Values.Where(x => x.Date >= (DateTime)dateStart && x.Date <= (DateTime)dateEnd);
                }
                else // only dateStart sended = return exact value
                {
                    result = holder.Values.Where(x => x.Date == (DateTime)dateStart);
                }

                if (result.Count<DataAndTemp>() > 0)
                    return Ok(result);
                else
                    return NoContent();
            }
            else // no data sended = return all data
            {
                return Ok(holder.Values);
            }
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime? date, [FromQuery] int? newValue)
        {
            if (date != null && newValue != null)
            {
                bool founded = false;
                for (int i = 0; i < holder.Values.Count; i++)
                {
                    if (holder.Values[i].Date == date)
                    {
                        holder.Values[i].Temperature = (int)newValue;
                        founded = true;
                    }
                }

                if (!founded)
                    return NoContent();

                return Ok();
            }
            else // no data for update = error
            {
                return BadRequest();
            }            
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime? dateStart, [FromQuery] DateTime? dateEnd)
        {            
            if (dateStart != null)
            {
                if (dateEnd != null) // with dateEnd = delete range
                {
                    for (int i = 0; i < holder.Values.Count; i++)
                    {
                        if (holder.Values[i].Date >= dateStart && holder.Values[i].Date <= dateEnd)
                        {
                            holder.Values.RemoveAt(i);
                        }
                    }
                    return Ok();
                }
                else // only dateStart sended = exact dataTime to delete
                {
                    for (int i = 0; i < holder.Values.Count; i++)
                    {
                        if (holder.Values[i].Date == dateStart)
                        {
                            holder.Values.RemoveAt(i);
                        }
                    }
                    return Ok();
                }               
            }
            else // no data sended = error
            {
                return BadRequest();
            }
        }
    }
}
