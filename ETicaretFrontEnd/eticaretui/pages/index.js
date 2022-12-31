import Navbar from "../components/header/Navbar";
import ProductList from "../components/products/ProductList";
import Sidebar from "../components/sidebar/Sidebar";
import axios from "axios";
import { useCallback, useEffect, useState } from "react";
import { toast } from "react-hot-toast";

export default function Home() {
  const [products, setProducts] = useState(null);
  const [filteredProducts, setFilteredProducts] = useState(null);

  const [brandFilter, setBrandFilter] = useState([]);
  const [brands, setBrands] = useState(null);
  const getProducts = useCallback(async () => {
    try {
      const response = await axios.get("https://localhost:7007/api/Computers/GetAll")
      setProducts(response.data.data)
      console.log(response.data.data);
      setBrands([...new Set(response.data.data?.map(item => item.brand))])
      // .then(response => setProducts(response.data.data))
      // .catch(error => toast.error(error.response.data.message ?? error.message))
    } catch (error) {
      toast.error(error?.response?.data?.message ?? error.message);
    }
  }, [])
  useEffect(() => {
    getProducts()
  }, [getProducts])

  useEffect(() => {

    setFilteredProducts(products?.filter(p => brandFilter.includes(p.brand)));

  }, [brandFilter])

  const handleSort = (srt) => {
    if (srt == "desc")
      setProducts([...products.sort(function (a, b) { return (a.priceByStore[0].price > b.priceByStore[0].price) ? 1 : ((b.priceByStore[0].price > a.priceByStore[0].price) ? -1 : 0); })]);
    else
      setProducts([...products.sort(function (a, b) { return (a.priceByStore[0].price < b.priceByStore[0].price) ? 1 : ((b.priceByStore[0].price < a.priceByStore[0].price) ? -1 : 0); })]);
  }
  return (
    <>
      <div className="container-fluid mt-5">
        <div className="row">
          <button className="btn btn-sm btn-outline-secondary" onClick={() => handleSort("desc")}>Fiyata Göre Küçükten Büyüğe Sırala</button>
          <button className="btn btn-sm btn-outline-secondary" onClick={() => handleSort()}>Fiyata Göre Büyükten Küçüğe Sırala</button>
          <div className="col-md-3">
            <Sidebar brands={brands} setBrandFilter={setBrandFilter} brandFilter={brandFilter} />
          </div>
          <div className="col-md-9">
            <ProductList products={filteredProducts?.length > 0 ? filteredProducts : products} />
          </div>

        </div>
      </div>
    </>
  )
}
