<template>
  <div class="home">
    <navigation-bar />
    <v-content>
        <v-container>
            <v-layout justify-flex-start align-flex-bottom wrap>
                <v-flex xs12 sm12 md3 pa-1 v-for="keep in keeps" :key="keep.id">
                  <keeps :keep="keep" />
                </v-flex>
            </v-layout>
        </v-container>
    </v-content>
  </div>
</template>

<script>
import NavigationBar from "@/components/NavigationBar";
import Keeps from "@/components/Keeps";
export default {
  name: "home",
  mounted() {
    //blocks users not logged in
    if (!this.$store.state.user.id) {
      this.$router.push({ name: "login" });
    }
    this.$store.dispatch("getKeeps", 0); //will have to increment argument. maybe in store is better idea anyway
  },
  computed: {
    keeps() {
      return this.$store.state.keeps;
    }
  },
  components: {
    NavigationBar,
    Keeps
  }
};
</script>