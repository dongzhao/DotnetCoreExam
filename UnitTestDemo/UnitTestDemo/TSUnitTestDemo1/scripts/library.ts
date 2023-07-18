var jqtest = {
    showMsg: function (): void {
        let v: any = jQuery.fn.jquery.toString();
        let conent: any = $("#ts-example-2")[0].innerHTML;
        $("#ts-example-2")[0].innerHTML = conent + "" + v + "!!";
    }
};

jqtest.showMsg();

function getData(value) {
    if (value > 1) {
        return true;
    }
};

module.exports = getData;

