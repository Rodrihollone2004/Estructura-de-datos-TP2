public abstract class TDA<T> 
{
    public abstract bool Add(T element);  //Agrega un elemento al conjunto 
    public abstract bool Remove(T element); //Elimina un elemento del conjunto
    public abstract bool Contains(T element); //Verifica si el conjunto contiene un elemento
    public abstract string Show(); //Muestra todos los elementos del conjunto
    public abstract int Cardinality(); //Devuelve la cardinalidad del conjunto (tamaño actual)
    public abstract bool IsEmpty(); //Verifica si el conjunto está vacío
    public abstract T GetElement(int index); //Para acceder al elemento dentro del otro nodo //Este lo agregué yo
    public abstract TDA<T> Union(TDA<T> otherSet); //Realiza la unión de 2 conjuntos 
    public abstract TDA<T> Intersection(TDA<T> otherSet); //Realiza la intersección de 2 conjuntos
    public abstract TDA<T> Difference(TDA<T> otherSet); //Realiza la diferencia entre 2 conjuntos
}
