## DpLIS Problem

$$
mi = max(j\ |\ max(|\{last(\{dp(j)\}) < a_i \}|)) \qquad (j < i) \\
$$


$$
\left \{
\begin{aligned}
	\{a_i\} & \qquad (i=0) \\
	\{dp(i-1), a_i\} & \qquad (i>0,\ \not \exist mi) \\
	\{dp(mi), a_i\} & \qquad (i>0,\ \exist mi,\ mi=i-1) \\
	\{x\ |\ min(last(\{ x=max(|\{dp(i-1)\}|,|\{dp(mi),a_i\}|)\}))\} & \qquad (i>0,\ \exist mi, \ mi<i-1) \\
\end{aligned}
\right.
$$
















