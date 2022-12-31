import Link from "next/link";
import { useRouter } from "next/router";
import { useCallback, useEffect, useState } from "react";
import toast from "react-hot-toast";
import axios from "axios";


export default function Navbar() {
    const [term, setTerm] = useState(null);
    const router = useRouter();
    const [results, setResults] = useState([]);
    const [show, setShow] = useState(false);
    const search = useCallback(async () => {
        try {
            if (!router.isReady) return;
            const response = await axios.get("https://localhost:7007/api/Computers/Search/" + term)
            setResults(response.data.data)
        } catch (error) {
            toast.error(error?.response?.data?.message ?? error.message);
        }
    }, [term])

    useEffect(() => {
        if (term?.length > 3) {
            search();
            setShow(true)
        }
    }, [search])

    useEffect(() => {
        setShow(false);
    }, [router.asPath])
    return (
        <nav className="navbar navbar-expand-lg bg-light">
            <div className="container-fluid">
                <Link className="navbar-brand fw-bold" href="/">E-Ticaret</Link>
                <div className="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                        <li className="nav-item">
                            <a className="nav-link active" aria-current="page" href="#">Ana Sayfa</a>
                        </li>
                        <li className="nav-item">
                            <Link href="/admin" passHref>
                                <a className="nav-link">Admin</a>
                            </Link>
                        </li>

                    </ul>
                    <form className="d-flex w-75 position-relative" role="search">
                        <input className="form-control" onChange={(e) => setTerm(e.target.value)} type="search" placeholder="Search" aria-label="Search" />
                        {/* <button className="btn btn-outline-success" type="submit">Search</button> */}
                        {(results?.length > 0 && term && term?.length > 3 && show) && <div className="position-absolute w-100" style={{ top: "45px", zIndex: "99999" }}>
                            <div className="card">
                                <div className="card-header fw-bold">Sonuçlar</div>
                                <div className="card-body">
                                    {results.map(result => <Link key={result.id} href={`/product/${result.id}`}><div style={{ cursor: "pointer" }} className="d-block border-bottom fw-bold">{result.caption}</div></Link>)}
                                </div>
                            </div>
                        </div>}
                    </form>
                </div>
            </div>

        </nav>
    )
}