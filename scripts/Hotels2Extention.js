var Hotels2Extention = function () {
    return {
        Interval: {
            IntervalHandleNumber: 0,
            IntervalFunction: null,
            IntervalTime: 1000,
            TimeActive: new Date(),
            Status: 0,
            Start: function (fn_major, time_major) {
                if (typeof (time_major) == "number") this.IntervalTime = time_major;
                if (typeof (fn_major) == "function") this.IntervalFunction = fn_major;
                if (typeof (this.IntervalFunction) == "function") {
                    var owner = this;
                    this.IntervalHandleNumber = setInterval(this.IntervalFunction, time_major);
                    this.Error = "";
                    this.Status = 1;
                    return this;
                } else {
                    this.Error = "Parameter Error";
                    this.Status = 0;
                    return this;
                }
            },
            Stop: function (_IntervalHandleNumber) {
                if (_IntervalHandleNumber != null) {
                    clearInterval(_IntervalHandleNumber);
                } else {
                    clearInterval(this.IntervalHandleNumber);
                }
                this.Status = 0;
            }
        }
    }
}