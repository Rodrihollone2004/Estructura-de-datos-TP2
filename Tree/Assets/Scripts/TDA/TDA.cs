public abstract class TDA 
{
    public abstract bool Add(int element);  //Agrega un elemento al conjunto 
    public abstract bool Remove(int element); //Elimina un elemento del conjunto
    public abstract bool Contains(int element); //Verifica si el conjun to contiene un elemento
    public abstract string Show(); //Muestra todos los elementos del conjunto
    public abstract int Cardinality(); //Devuelve la cardinalidad del conjunto (tamaño actual)
    public abstract bool isEmpty(); //Verifica si el conjunto está vacío
    public abstract int GetElement(int index); //Para acceder al elemento dentro del otro nodo //Este lo agregué yo
    public abstract TDA Union(TDA otherSet); //Realiza la unión de 2 conjuntos 
    public abstract TDA Intersection(TDA otherSet); //Realiza la intersección de 2 conjuntos
    public abstract TDA Difference(TDA otherSet); //Realiza la diferencia entre 2 conjuntos
}
