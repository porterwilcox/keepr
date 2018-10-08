<template>
    <v-dialog v-model="viewKeep" transition="scale-transition" width="fit-content">
        <v-btn @click="getAuthor(keep.userId)" slot="activator" flat fab small color="amber darken-4">view</v-btn>
        <v-layout justify-center>
            <v-card v-if="keep.img.length > 0" width="80vw" height="80vh">
                <v-toolbar card color="cyan lighten-4">
                    <v-layout column fill-height justify-center align-start>
                        <v-card-title class="img-title">{{keep.name}}</v-card-title>
                        <v-btn flat small>{{author.username}}</v-btn>
                    </v-layout>
                    <v-spacer></v-spacer>
                    <v-toolbar-items>
                        <v-chip outline color="amber darken-4">
                            {{keep.views}}<v-icon right>fa-eye</v-icon>
                        </v-chip>
                    </v-toolbar-items>
                </v-toolbar>
                <v-img v-if="keep.img.length > 0" :src="keep.img" class="white--text" width="100%" height="100%"></v-img>
            </v-card>
            <v-card v-else width="40vw" height="fit-content">
                <v-toolbar card color="cyan lighten-4">
                    <v-layout column fill-height justify-center align-start>
                        <v-card-title class="img-title">{{keep.name}}</v-card-title>
                        <v-btn flat small>{{author.username}}</v-btn>
                    </v-layout>
                    <v-spacer></v-spacer>
                    <v-toolbar-items>
                        <v-chip outline color="amber darken-4">
                            {{keep.views}}<v-icon right>fa-eye</v-icon>
                        </v-chip>
                    </v-toolbar-items>
                </v-toolbar>
            </v-card>
        </v-layout>
    </v-dialog>
</template>
<script>
export default {
  name: "viewKeep",
  data() {
    return {
      viewKeep: false
    };
  },
  props: ["keep"],
  computed: {
    author() {
      return this.$store.state.author;
    }
  },
  methods: {
    getAuthor(userId) {
      console.log(userId);
      this.$store.dispatch("getAuthor", userId);
    }
  }
};
</script>
<style>
</style>