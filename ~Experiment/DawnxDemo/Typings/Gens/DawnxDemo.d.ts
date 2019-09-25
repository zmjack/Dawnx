
declare namespace DawnxDemo.Models {
	export const enum EState {
		Ready = 0,
		Running = 1,
		Complete = 2
	}
	interface AAA {
		A: string;
		B: string;
		DT: DawnxDemo.Models.AAA.BBB;
		State: DawnxDemo.Models.EState;
	}
}
declare namespace DawnxDemo.Models.AAA {
	interface BBB {
		A: string;
		B: string;
		State: DawnxDemo.Models.EState;
	}
}
