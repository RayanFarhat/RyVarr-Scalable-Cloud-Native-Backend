<script>
    import { page } from '$app/stores';  
</script>

<div class="flex-none hidden lg:block">
    <ul class="menu menu-horizontal">
      <!-- Navbar menu content here -->

      <li><a class="p-0 hover:bg-transparent" href="/pricing">
        <button class="btn btn-outline border-0 hover:bg-transparent hover:text-primary">
        Pricing</button></a></li>

      <li><a class=" mr-12 p-0 hover:bg-transparent" href="/hamzacad">
        <button class="btn btn-outline border-0 hover:bg-transparent hover:text-primary">
        Hamzacad</button></a></li>

        <li><a class="p-0 hover:bg-transparentt" href="/login">
          <button class="btn btn-outline btn-primary">Login</button></a></li>

      <li><a class="p-0 hover:bg-transparentt" href="/2">
        <button class="btn btn-primary">{
          $page.url.pathname.includes("/hamzacad")? "Download":"Let's Talk"
          }</button></a></li>
    </ul>
  </div>