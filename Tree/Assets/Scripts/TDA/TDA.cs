public abstract class TDA 
{
    public abstract bool Add(int element);  //Agrega un elemento al conjunto 
    public abstract bool Remove(int element); //Elimina un elemento del conjunto
    public abstract bool Contains(int element); //Verifica si el conjunto contiene un elemento
    public abstract void Show(); //Muestra todos los elementos del conjunto
    public abstract int Cardinality(); //Devuelve la cardinalidad del conjunto (tama�o actual)
    public abstract bool isEmpty(); //Verifica si el conjunto est� vac�o
    public abstract int GetElementAt(int index); //Verifica si el conjunto est� vac�o //Este lo agregu� yo
    public abstract TDA Union(TDA otherSet); //Realiza la uni�n de 2 conjuntos 
    public abstract TDA Intersection(TDA otherSet); //Realiza la intersecci�n de 2 conjuntos
    public abstract TDA Difference(TDA otherSet); //Realiza la diferencia entre 2 conjuntos
}
