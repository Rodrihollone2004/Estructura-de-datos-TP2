public abstract class TDA 
{
    public abstract bool Add(int element);  //Agrega un elemento al conjunto 
    public abstract bool Remove(int element); //Elimina un elemento del conjunto
    public abstract bool Contains(int element); //Verifica si el conjunto contiene un elemento
    public abstract void Show(); //Muestra todos los elementos del conjunto
    public abstract int Cardinality(); //Devuelve la cardinalidad del conjunto (tamaño actual)
    public abstract bool isEmpty(); //Verifica si el conjunto está vacío
    public abstract int GetElementAt(int index); //Verifica si el conjunto está vacío //Este lo agregué yo
    public abstract TDA Union(TDA otherSet); //Realiza la unión de 2 conjuntos 
    public abstract TDA Intersection(TDA otherSet); //Realiza la intersección de 2 conjuntos
    public abstract TDA Difference(TDA otherSet); //Realiza la diferencia entre 2 conjuntos
}
