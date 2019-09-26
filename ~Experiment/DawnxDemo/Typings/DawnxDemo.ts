namespace DawnxDemo.Models {
    export namespace AAA {
        export const AAAA12 : string = '123';
    }
}

declare namespace DawnxDemo.Models {
    export const enum EState {
        Ready = 0,
        Running = 1,
        Complete = 2,
    }
    interface BBB {
        state? : DawnxDemo.Models.EState;
        a? : string;
        b? : string;
    }
    interface AAA {
        state? : DawnxDemo.Models.EState;
        a? : string;
        b? : string;
        dt? : DawnxDemo.Models.BBB;
    }
}

