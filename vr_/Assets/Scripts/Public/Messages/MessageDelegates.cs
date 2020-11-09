namespace Public
{
    public delegate void DelegateT();
    public delegate void DelegateT<in T1>(T1 t1);
    public delegate void DelegateT<in T1, in T2>(T1 t1, T2 t2);
    public delegate void DelegateT<T1, T2, T3>(T1 t1, T2 t2, T3 t3);
    public delegate void DelegateT<T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4);
}
