function getDate(ds)
{
    alert("Sent - "+ ds);
    if (navigator.userAgent.indexOf('Chrome') != -1)
    {
        var strdt = ds.toString().split("-");
        var fdt = new Date(parseInt(strdt[0], 10), parseInt(strdt[1], 10) - 1, parseInt(strdt[2], 10));
        
    }
    else
    {
        var strdt = ds.toString("dd/MM/yyyy").split("/");
        var fdt = new Date(parseInt(strdt[2], 10), parseInt(strdt[1], 10) - 1, parseInt(strdt[0], 10));
    }
    alert("returned - " + fdt);
    return fdt;
}
function setDate(ds)
{
    if (navigator.userAgent.indexOf('Chrome') != -1)
    {
        var rd = ds.toString("yyyy-MM-dd");
    }
    else
    {
        var rd = ds.toString("dd/MM/yyyy");
    }

    return rd;
}