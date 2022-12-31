
import Navbar from '../components/header/Navbar';
export default function HomeLayout({ children }) {
    return (
        <div>
            <Navbar />
            {children}
        </div>
    )
}
